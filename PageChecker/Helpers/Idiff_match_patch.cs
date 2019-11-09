﻿using System;
using System.Collections.Generic;

namespace PageCheckerAPI.Helpers
{
    public interface Idiff_match_patch
    {
        List<Diff> diff_main(string text1, string text2);
        List<Diff> diff_main(string text1, string text2, bool checklines);
        int diff_commonPrefix(string text1, string text2);
        int diff_commonSuffix(string text1, string text2);
        void diff_cleanupSemantic(List<Diff> diffs);
        void diff_cleanupSemanticLossless(List<Diff> diffs);
        void diff_cleanupEfficiency(List<Diff> diffs);
        void diff_cleanupMerge(List<Diff> diffs);
        int diff_xIndex(List<Diff> diffs, int loc);
        string diff_prettyHtml(List<Diff> diffs);
        string diff_text1(List<Diff> diffs);
        string diff_text2(List<Diff> diffs);
        int diff_levenshtein(List<Diff> diffs);
        string diff_toDelta(List<Diff> diffs);
        List<Diff> diff_fromDelta(string text1, string delta);
        int match_main(string text, string pattern, int loc);
        List<Patch> patch_make(string text1, string text2);
        List<Patch> patch_make(List<Diff> diffs);

        List<Patch> patch_make(string text1, string text2,
            List<Diff> diffs);

        List<Patch> patch_make(string text1, List<Diff> diffs);
        List<Patch> patch_deepCopy(List<Patch> patches);
        Object[] patch_apply(List<Patch> patches, string text);
        string patch_addPadding(List<Patch> patches);
        void patch_splitMax(List<Patch> patches);
        string patch_toText(List<Patch> patches);
        List<Patch> patch_fromText(string textline);
    }
}